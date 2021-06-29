<%@ page import="com.liupanlong.domain.User" %>
<%@ page import="java.util.List" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<link href="css/bootstrap.min.css" rel="stylesheet"/>
<html>
<head>
    <title>查询列表</title>
</head>
        <%
            if(request.getAttribute("delete") != null){
        %>
                <c:if test="${delete}">
                <script>
                    alert("删除成功");
                </script>
                </c:if>
                <c:if test="${not delete}">
                <script>
                    alert("删除失败");
                </script>
                </c:if>
        <%
            }
        %>
        <%
            if(request.getAttribute("update") != null){
        %>
        <c:if test="${update}">
            <script>
                alert("修改成功");
            </script>
        </c:if>
        <c:if test="${not update}">
            <script>
                alert("修改失败");
            </script>
        </c:if>
        <%
            }
        %>
<style>
    th, td{
        text-align: center;
    }
</style>
<body>
<div class="container">
    <h3 style="text-align: center">用户信息列表</h3>
    <table class="table table-bordered table-hover">
        <tr class="success">
            <th>编号</th>
            <th>姓名</th>
            <th>性别</th>
            <th>年龄</th>
            <th>地址</th>
            <th>QQ</th>
            <th>邮箱</th>
            <th>操作</th>
        </tr>
        <c:forEach items="${users}" var="user" varStatus="s">
            <tr>
                <td>${s.count}</td>
                <td>${user.name}</td>
                <td>${user.gender}</td>
                <td>${user.age}</td>
                <td>${user.address}</td>
                <td>${user.qq}</td>
                <td>${user.email}</td>
                <td>
                    <form action="" method="post">
                    <a class="btn btn-sm btn-success" method="post" href="update.jsp?id=${user.id}&name=${user.name}&gender=${user.gender}&age=${user.age}&address=${user.address}&qq=${user.qq}&email=${user.email}">修改</a>&nbsp;
                    <a class="btn btn-danger btn-sm" href="${pageContext.request.contextPath}/userDeleteServlet?id=${user.id}">删除</a>
                    </form>
                </td>
            </tr>
        </c:forEach>
        <tr>
            <td colspan="8" align="center"><a class="btn btn-primary" href="add.jsp">添加联系人</a></td>
        </tr>
    </table>
</div>
</body>
</html>
