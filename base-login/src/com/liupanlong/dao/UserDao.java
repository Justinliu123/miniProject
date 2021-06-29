package com.liupanlong.dao;

import com.liupanlong.domain.User;

import java.util.List;

public interface UserDao {
    public List<User> findAll();
    public boolean insert(User user);
    public boolean update(User user);
    public boolean delete(int id);
}
