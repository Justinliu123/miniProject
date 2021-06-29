package com.liupanlong.web.servlet;

import com.liupanlong.domain.User;
import com.liupanlong.service.UserService;
import com.liupanlong.service.impl.UserServiceImpl;

import javax.jms.Session;
import javax.servlet.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;
import java.io.IOException;
import java.util.List;

@WebServlet("/userListServlet")
public class UserListServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        doPost(request, response);
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        //查询所有用户信息
        UserService service = new UserServiceImpl();
        List<User> users = service.findAll();
        //存到request域中
        request.setAttribute("users", users);
        //转发到页面
        request.getRequestDispatcher("/list.jsp").forward(request,response);
    }
}
