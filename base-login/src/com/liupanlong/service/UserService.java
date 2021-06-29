package com.liupanlong.service;

import com.liupanlong.domain.User;

import java.util.List;

public interface UserService {
    /**
     * 查询所有用户信息
     * @return
     */
    public List<User> findAll();
    public boolean insert(User user);
    public boolean update(User user);
    public boolean delete(int id);
}
