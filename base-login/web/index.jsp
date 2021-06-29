<%--
  Created by IntelliJ IDEA.
  User: 86135
  Date: 2021/5/11
  Time: 21:20
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page import="com.liupanlong.domain.Admin" %>
<%@ page import="java.util.Map" %>
<%@ page import="java.util.HashMap" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<html>
  <head>
    <title>首页</title>
    <link href="css/bootstrap.min.css" rel="stylesheet"/>、
  </head>

  <body>
  <div align="center">
      <h1>
          ${sessionScope['admin'].username},您好！
      </h1>
      <a href="${pageContext.request.contextPath}/userListServlet" class="btn btn-link"><h1 style="color: #1b6d85">查询所有用户信息</h1>
      </a>
  </div>

  </body>
</html>
