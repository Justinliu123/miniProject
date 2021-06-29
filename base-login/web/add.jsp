<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<html lang="zh-CN">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<!-- viewport视口：网页可以根据设置的宽度自动进行适配，在浏览器的内部虚拟一个容器，容器的宽度与设备的宽度相同。
width: 默认宽度与设备的宽度相同
initial-scale: 初始的缩放比，为1:1 -->
<meta name="viewport" content="width=device-width, initial-scale=1">
<head>
    <title>添加用户</title>
    <!-- 1. 导入CSS的全局样式 -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <!-- 2. jQuery导入，建议使用1.9以上的版本 -->
    <script src="js/jquery-3.5.1.min.js"></script>
    <!-- 3. 导入bootstrap的js文件 -->
    <script src="js/bootstrap.min.js"></script>
    <%
        if(request.getAttribute("insert") != null){
    %>
        <c:if test="${insert}">
            <script>
                alert("插入成功");
            </script>
        </c:if>
        <c:if test="${not insert}">
            <script>
                alert("插入失败");
            </script>
        </c:if>
    <%
        }
    %>
    <style>
        #title{
            text-align: center;
        }
    </style>
    <script>
        /*
            姓名不能为空
            检查age范围
            邮箱格式检验
        * */
    </script>
</head>
<body>
<div class="container" style="width: 400px;">
    <h3 id="title">添加联系人页面</h3>
    <form action="${pageContext.request.contextPath}/userInsertServlet" method="post">
        <div class="form-group">
            <label for="name">姓名：</label>
            <input type="text" class="form-control" id="name" name="name" placeholder="请输入姓名">
        </div>

        <div class="form-group">
            <label>性别：</label>
            <input type="radio" name="gender" value="男" checked="checked"/>男
            <input type="radio" name="gender" value="女"/>女
        </div>

        <div class="form-group">
            <label for="age">年龄：</label>
            <input type="number" max="180" min="0" class="form-control" id="age" name="age" placeholder="请输入年龄">
        </div>

        <div class="form-group">
            <label for="address">籍贯：</label>
            <%--<select id="address" name="address" class="form-control" id="jiguan">
                <option value="广东">广东</option>
                <option value="广西">广西</option>
                <option value="湖南">湖南</option>
            </select>--%>
            <input id="address" type="text" class="form-control" name="address" placeholder="请输入籍贯"/>
        </div>

        <div class="form-group">
            <label for="qq">QQ：</label>
            <input id="qq" type="text" class="form-control" name="qq" placeholder="请输入QQ号码"/>
        </div>

        <div class="form-group">
            <label for="email">Email：</label>
            <input id="email" type="text" class="form-control" name="email" placeholder="请输入邮箱地址"/>
        </div>

        <div class="form-group" style="text-align: center">
            <input class="btn btn-primary" type="submit" value="提交" />
            <input class="btn btn-default" type="reset" value="重置" />
            <a class="btn btn-default" href="${pageContext.request.contextPath}/userListServlet">返回</a>
        </div>
    </form>
</div>
</body>
</html>
