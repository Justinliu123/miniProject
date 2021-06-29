package com.liupanlong.web.servlet;

import com.liupanlong.domain.User;
import com.liupanlong.service.UserService;
import com.liupanlong.service.impl.UserServiceImpl;

import javax.servlet.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;
import java.io.IOException;

@WebServlet("/userDeleteServlet")
public class UserDeleteServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        doPost(request, response);
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        int id = Integer.parseInt(request.getParameter("id"));
        UserServiceImpl userService = new UserServiceImpl();
        if(userService.delete(id)){
            request.setAttribute("delete", true);
        }else{
            request.setAttribute("delete", false);
        }
        request.getRequestDispatcher("/userListServlet").forward(request, response);
    }
}
