package com.liupanlong.test2.service.impl;

import com.liupanlong.test2.mapper.UserMapper;
import com.liupanlong.test2.pojo.User;
import com.liupanlong.test2.pojo.UserExample;
import com.liupanlong.test2.pojo.vo.backUser;
import com.liupanlong.test2.service.UserService;
import com.liupanlong.test2.util.IdWorker;
import com.liupanlong.test2.util.JwtUtil;
import io.jsonwebtoken.Claims;
import org.springframework.beans.BeanUtils;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.data.redis.core.StringRedisTemplate;
import org.springframework.data.redis.serializer.StringRedisSerializer;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.util.DigestUtils;

import java.util.List;
import java.util.concurrent.TimeUnit;

@Service
@Transactional
public class UserServiceImpl implements UserService {

    @Autowired
    private UserMapper userMapper;

    @Autowired
    private IdWorker idWorker;

    @Autowired
    private StringRedisTemplate redisTemplate;

    @Override
    public backUser login(String username, String password) {

        if(StringUtils.isNotBlank(username) && StringUtils.isNotBlank(password)){
            UserExample example = new UserExample();
            UserExample.Criteria criteria = example.createCriteria();

            criteria.andUsernameEqualTo(username);

            List<User> userList = userMapper.selectByExample(example);

            if (userList != null && userList.size() == 1){
                // 对密码进行校验
                String encodingPassword = DigestUtils.md5DigestAsHex(password.getBytes());
                //System.out.println(encodingPassword);
                //登录成功
                if (encodingPassword.equals(userList.get(0).getPassword())){
                    backUser user = new backUser();                         //返回查询到得用户，不包括密码
                    BeanUtils.copyProperties(userList.get(0), user);

                    long outTime = 1000*60*60*24;
                    //获取JWT token
                    String token = JwtUtil.createJWT(username,outTime, username);
                    //System.out.println("存储在客户端的" + token);
                    user.setToken(token);
                    //将登录的token存入redis中
                    redisTemplate.opsForValue().set(token, username);
                    redisTemplate.expire(token, outTime, TimeUnit.MILLISECONDS);            //设置超时时间
                    return user;
                }else{
                    throw new RuntimeException("用户密码错误");
                }
            }else{
                throw new RuntimeException("用户名不存在！");
            }
        }
        return null;
    }

    @Override
    public void register(User user) {
        try {
            // 1.判断用户名是否存在
            UserExample example = new UserExample();
            UserExample.Criteria criteria = example.createCriteria();

            criteria.andUsernameEqualTo(user.getUsername());

            List<User> userList = userMapper.selectByExample(example);
            if (userList != null && userList.size() > 0){
                throw new RuntimeException("用户已存在");
            }

            // 2.将用户信息保存到数据库中
            // 使用雪花算法来生成唯一ID
            user.setId(idWorker.nextId());
            // 对密码进行md5加密
            user.setPassword(DigestUtils.md5DigestAsHex(user.getPassword().getBytes()));
            userMapper.insert(user);

        } catch (Exception e) {
            e.printStackTrace();
            if(!e.getMessage().equals("用户已存在")){
                throw new RuntimeException("注册失败");
            }else {
                throw new RuntimeException("用户已存在");
            }
        }
    }

    @Override
    public Boolean checkToken(String token) {
        String username = (String) redisTemplate.opsForValue().get(token);
        //System.out.println(username);
        Claims claims = JwtUtil.parseJWT(username, token);
        //System.out.println("claims "+claims);
        if(username.equals(claims.get("userid"))){
            return true;
        }
        return false;
    }

}
