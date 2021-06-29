package com.liupanlong.test2.service;

import com.liupanlong.test2.pojo.User;
import com.liupanlong.test2.pojo.vo.backUser;
import com.liupanlong.test2.pojo.vo.receiveUser;

public interface UserService {
    /**
     * 用来检查用户名和密码是否匹配
     * @param username 用户名
     * @param password 密码
     * @return 如果登录成功返回用户对象，否则返回null
     */
    backUser login(String username, String password);

    /**
     * 注册用户，将用户信息保存到数据库中
     * 如果抛出异常则注册失败，否则注册成功
     * @param user
     */
    void register(User user);

    Boolean checkToken(String token);
}
