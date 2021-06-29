package com.liupanlong.dao.impl;

import com.liupanlong.domain.Admin;
import com.liupanlong.util.JDBCUtils;
import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.JdbcTemplate;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.List;

/**
 * 操作数据库中的user
 */
public class AdminDao {
    //共用的jdbcTemplate
    JdbcTemplate je;
    public Admin login(Admin loginUser){
        try {
            je = new JdbcTemplate(JDBCUtils.getDataSource());
            String sql = "select * from user where username = ? and password = ?;";

            Admin resultUser = je.queryForObject(sql, new BeanPropertyRowMapper<Admin>(Admin.class),
                    loginUser.getUsername(), loginUser.getPassword());
            return resultUser;
        }catch (DataAccessException e){
            return null;
        }
    }
}
