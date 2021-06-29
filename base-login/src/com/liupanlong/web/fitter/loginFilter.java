package com.liupanlong.web.fitter;

import javax.servlet.*;
import javax.servlet.annotation.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@WebFilter(filterName = "loginFilter", value = "/*")
public class loginFilter implements Filter {
    public void init(FilterConfig config) throws ServletException {
    }

    public void destroy() {
    }

    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain) throws ServletException, IOException {
        HttpServletResponse resp = (HttpServletResponse) response;
        HttpServletRequest req = (HttpServletRequest) request;
        String uri = req.getRequestURI();
        /*
        * 不需要拦截的资源。注意：css/js/验证码等资源
        * */
        if(uri.contains("/login.jsp") || uri.contains("/loginServlet") || uri.contains("/checkCodeServlet")
                || uri.contains("/css/") || uri.contains("/fonts/") || uri.contains("/js/"))    //不需要拦截
        {
            //放行
            chain.doFilter(req, resp);
        }
        else    //需要拦截
        {
            //判断是否登录
            Object o = req.getSession().getAttribute("admin");
            if(o != null){
                chain.doFilter(req, resp);
            }
            else{
                req.getRequestDispatcher("/loginDoor.jsp").forward(req,resp);
            }
        }
        //chain.doFilter(request, response);
    }
}
