package com.liupanlong.test2.controller;

import com.liupanlong.test2.pojo.User;
import com.liupanlong.test2.pojo.vo.Result;
import com.liupanlong.test2.pojo.vo.backUser;
import com.liupanlong.test2.pojo.vo.receiveUser;
import com.liupanlong.test2.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.http.HttpServletRequest;

@RestController
@RequestMapping("/user")
public class UserController {
    @Autowired
    private UserService userService;

    @RequestMapping("/login")
    public Result login(@RequestBody receiveUser user){
        //System.out.println(user);
        try{
            backUser _user = userService.login(user.getUsername(), user.getPassword());
            if(_user != null){
                return new Result(true, "登录成功", _user);
            }
        }catch (RuntimeException e){
            e.printStackTrace();
            return new Result(false, e.getMessage());
        }
        return new Result(false, "未知错误");
    }

    @RequestMapping("/register")
    public Result register(@RequestBody User user){
        // 注册成功，不抛出异常，注册失败抛出异常
        try{
            userService.register(user);
            return new Result(true, "注册成功");
        } catch (RuntimeException e){
            e.printStackTrace();
            return new Result(false, e.getMessage());
        }
    }

    @RequestMapping("/checkToken")
    public Boolean checkToken(HttpServletRequest request){
        String token = request.getHeader("token");
        //System.out.println(token);
        try{
            return userService.checkToken(token);
        }catch (Exception e){
            e.printStackTrace();
            return false;
        }

    }
}
