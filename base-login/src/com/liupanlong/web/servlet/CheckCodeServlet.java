package com.liupanlong.web.servlet;

import com.liupanlong.util.VCodeUtils;

import javax.imageio.ImageIO;
import javax.jms.Session;
import javax.servlet.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;
import java.awt.image.BufferedImage;
import java.io.IOException;

@WebServlet("/checkCodeServlet")
public class CheckCodeServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        doPost(request, response);
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        VCodeUtils vCodeUtils = new VCodeUtils();
        vCodeUtils.setNum(5);
        BufferedImage codeImg = vCodeUtils.getImage();
        ImageIO.write(codeImg, "jpeg", response.getOutputStream());
        request.getSession().setAttribute("checkCode",vCodeUtils.getText());
    }
}
