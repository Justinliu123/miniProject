package com.liupanlong.dao.impl;

import com.liupanlong.dao.UserDao;
import com.liupanlong.domain.User;
import com.liupanlong.util.JDBCUtils;
import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.JdbcTemplate;

import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.List;

public class UserDaoImpl implements UserDao {
    private JdbcTemplate jdbcTemplate = new JdbcTemplate(JDBCUtils.getDataSource());
    @Override
    public List<User> findAll() {
        String sql = "SELECT * FROM customer;";
        List<User> users = jdbcTemplate.query(sql, new BeanPropertyRowMapper<User>(User.class));
        return users;
    }

    @Override
    public boolean insert(User user) {
        String sql = "insert into customer(name, gender, age, address, qq, email) values(?, ?, ?, ?, ?, ?)";
        int res = jdbcTemplate.update(sql, user.getName(), user.getGender(), user.getAge(), user.getAddress(), user.getQq(), user.getEmail());
        return res > 0 ? true: false;
    }

    @Override
    public boolean update(User user) {
        String sql = "update customer set name=?, gender=?, age=?, address=?, qq=?, email=? where id=?";
        int res = jdbcTemplate.update(sql, user.getName(), user.getGender(), user.getAge(), user.getAddress(), user.getQq(), user.getEmail(), user.getId());
        return res > 0 ? true: false;
    }

    @Override
    public boolean delete(int id) {
        String sql = "delete from customer where id=?";
        int res = jdbcTemplate.update(sql, id);
        return res > 0 ? true: false;
    }
}
