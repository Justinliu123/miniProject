package com.liupanlong.service.impl;

import com.liupanlong.dao.UserDao;
import com.liupanlong.dao.impl.UserDaoImpl;
import com.liupanlong.domain.User;
import com.liupanlong.service.UserService;

import java.util.List;

public class UserServiceImpl implements UserService {
    private UserDao userDao = new UserDaoImpl();
    @Override
    public List<User> findAll() {
        return userDao.findAll();
    }

    @Override
    public boolean insert(User user) {
        if(user.getName() == null || user.getName().length() < 1){
            return false;
        }
        else if(user.getAge() > 180 || user.getAge() < 1){
            return false;
        }
        return userDao.insert(user);
    }

    @Override
    public boolean update(User user) {
        if(user.getName() == null || user.getName().length() < 1){
            return false;
        }
        else if(user.getAge() > 180 || user.getAge() < 1){
            return false;
        }
        return userDao.update(user);
    }

    @Override
    public boolean delete(int id) {
        return userDao.delete(id);
    }
}
