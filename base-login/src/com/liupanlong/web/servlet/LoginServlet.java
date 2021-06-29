package com.liupanlong.web.servlet;

import com.liupanlong.dao.UserDao;
import com.liupanlong.dao.impl.AdminDao;
import com.liupanlong.domain.Admin;
import com.liupanlong.domain.User;
import org.apache.commons.beanutils.BeanUtils;

import javax.servlet.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.util.HashMap;
import java.util.Map;

@WebServlet("/loginServlet")
public class LoginServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        doPost(req, resp);
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        req.setCharacterEncoding("UTF-8");
        /*String username = req.getParameter("username");
        String password = req.getParameter("password");
        User loginUser = new User(username,password);*/

        //将请求参数存在一个新的map中，先验证验证码
        Map<String, String[]> map = new HashMap(req.getParameterMap());
        String[] inCode = new String[1];   //输入的验证码
        if(map.containsKey("checkCode")){
            inCode = map.get("checkCode");
            map.remove("checkCode");
        }
        //判断验证码是否正确,错误重定向到主页
        String checkCode = (String)req.getSession().getAttribute("checkCode");
        req.getSession().removeAttribute("checkCode");
        if(checkCode == null || !checkCode.equalsIgnoreCase(inCode[0])){
            req.setAttribute("login_msg","验证码错误");
            req.getRequestDispatcher("/login.jsp").forward(req, resp);
            return;
        }
        //将填入的表单封装为User对象
        Admin loginAdmin = new Admin();
        try {
            BeanUtils.populate(loginAdmin, map);
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        }
        //和数据库中的信息比对
        Admin admin = (new AdminDao()).login(loginAdmin);
        if(null != admin){
            //将admin存到Session中
            HttpSession session = req.getSession();
            Cookie cookie = new Cookie("JSESSIONID", session.getId());
            cookie.setMaxAge(60*30);    //最大存活时间单位秒
            session.setAttribute("admin", admin);
            resp.addCookie(cookie);     //将创建的cookie添加到响应中
            resp.sendRedirect(req.getContextPath() + "/index.jsp");
        }else{
            req.setAttribute("login_msg","用户名或密码错误");
            req.getRequestDispatcher("/login.jsp").forward(req, resp);
        }
    }
}
