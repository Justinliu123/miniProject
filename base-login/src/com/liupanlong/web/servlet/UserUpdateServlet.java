package com.liupanlong.web.servlet;

import com.liupanlong.domain.User;
import com.liupanlong.service.UserService;
import com.liupanlong.service.impl.UserServiceImpl;
import org.apache.commons.beanutils.BeanUtils;

import javax.servlet.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.util.Map;

@WebServlet("/userUpdateServlet")
public class UserUpdateServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        doPost(request, response);
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        //修改request编码集
        request.setCharacterEncoding("UTF-8");
        Map<String, String[]> usermap= request.getParameterMap();
        User user = new User();
        try {
            BeanUtils.populate(user, usermap);
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        }
        UserService service = new UserServiceImpl();
        if(service.update(user)){
            request.setAttribute("update", true);
        }else{
            request.setAttribute("update", false);
        }
        request.getRequestDispatcher("/userListServlet").forward(request, response);
    }
}
