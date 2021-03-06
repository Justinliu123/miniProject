package com.liupanlong.chatRoom.controller;

import com.liupanlong.chatRoom.pojo.TbUser;
import com.liupanlong.chatRoom.pojo.vo.Result;
import com.liupanlong.chatRoom.pojo.vo.User;
import com.liupanlong.chatRoom.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;

@RestController
@RequestMapping("/user")
public class UserController {

    @Autowired
    private UserService userService;

    @RequestMapping("/findAll")
    public List<TbUser> findAll(){
        return userService.findAll();
    }

    @RequestMapping("/login")
    public Result login(@RequestBody TbUser user){
        try{
            User _user = userService.login(user.getUsername(), user.getPassword());
            if (_user == null){
                return new Result(false, "请检查用户名或密码是否正确！");
            }
            else {
                return new Result(true,"登录成功！",_user);
            }
        } catch (Exception e){
            e.printStackTrace();
            return new Result(false, "登录错误");
        }
    }

    @RequestMapping("/register")
    public Result register(@RequestBody TbUser user){
        // 注册成功，不抛出异常，注册失败抛出异常
        try{
            System.out.println("Yes");
            userService.register(user);
            return new Result(true,"注册成功");
        } catch (RuntimeException e){
            e.printStackTrace();
            return new Result(false,e.getMessage());
        }
    }

    @RequestMapping("/upload")
    public Result upload(MultipartFile file, String userid){
        try {
            User user = userService.upload(file,userid);
            if (user != null){
                //System.out.println(user);
                return new Result(true,"上传成功",user);
            } else {
                return new Result(false, "上传失败");
            }
        } catch (Exception e){
            e.printStackTrace();
            return new Result(false,"上传错误");
        }
    }

    @RequestMapping("/updateNickname")
    public Result updateNickname(@RequestBody TbUser user){
        try {
            userService.updateNickname(user.getId(),user.getNickname());
            return new Result(true,"更新成功");
        } catch (RuntimeException e){
            return new Result(false,e.getMessage());
        } catch (Exception e) {
            e.printStackTrace();
            return new Result(false,"更新失败");
        }
    }

    @RequestMapping("/findById")
    public User findById(String userid){
        return userService.findById(userid);
    }

    @RequestMapping("/findByUsername")
    public Result findByUsername(String userid,String friendUsername){
        try {
            User user = userService.findByUsername(userid,friendUsername);
            if (user != null){
                return new Result(true,"搜索成功",user);
            }
            return new Result(false,"没有找到该用户");
        } catch (Exception e) {
            e.printStackTrace();
            return new Result(false,"搜索失败");
        }
    }
}
