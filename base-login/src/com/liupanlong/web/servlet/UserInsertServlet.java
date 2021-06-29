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
import java.util.HashMap;
import java.util.List;
import java.util.Map;


@WebServlet("/userInsertServlet")
public class UserInsertServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        doPost(request, response);
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        //将request的编码修改
        request.setCharacterEncoding("UTF-8");
        //获取填写的表单信息, 表单中没有填写id
        UserService service = new UserServiceImpl();
        Map<String, String[]> usermap = request.getParameterMap();
        //将map转为user对象
        User user = new User();
        try {
            BeanUtils.populate(user, usermap);
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        }
        //在request域中设置插入属性，插入成功返回true, 插入失败返回false
        if(service.insert(user)){
            request.setAttribute("insert", true);
        }else{
            request.setAttribute("insert", false);
        }
        //转发到页面
        request.getRequestDispatcher("/add.jsp").forward(request, response);
    }
}
