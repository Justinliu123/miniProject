package com.liupanlong.web.fitter;


import javax.servlet.*;
import javax.servlet.annotation.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.*;
import java.lang.reflect.InvocationHandler;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Map;
import java.util.function.Consumer;
import java.util.function.Function;

@WebFilter("/*")
public class SensitiveWordsFilter implements Filter {
    List<String> strings = new ArrayList<>();
    public void init(FilterConfig config) throws ServletException {
        //获取sensitiveWords中的敏感词集
        //config.getServletContext().getRealPath("/WEB-INF/classes/sensitiveWords.txt");
        String path = SensitiveWordsFilter.class.getClassLoader().getResource("sensitiveWords.txt").getPath();
        BufferedReader br = null;
        try {
            br = new BufferedReader(new InputStreamReader(new FileInputStream(path),"utf-8" ));
            String line = null;
            while((line = br.readLine()) != null){
                strings.add(line);
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }finally {
            if(br != null){
                try {
                    br.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    public void destroy() {
    }

    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain) throws ServletException, IOException {
        //创建代理对象，增强getParameter
        ServletRequest proxy_req = (ServletRequest) Proxy.newProxyInstance(request.getClass().getClassLoader(), request.getClass().getInterfaces(), new InvocationHandler() {
            @Override
            public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {
                if(method.getName().equals("getParameter")){
                    String value = (String) method.invoke(request, args);
                    for(String str: strings){
                        if(value.contains(str)){
                            value = value.replaceAll(str, "***");
                        }
                    }
                    return value;
                }
                return method.invoke(request, args);
            }
        });
        chain.doFilter(proxy_req, response);
    }
}
