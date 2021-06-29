<%--
  Created by IntelliJ IDEA.
  User: 86135
  Date: 2021/5/8
  Time: 13:35
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<style>
  body{
    background: url("https://api.dujin.org/bing/1920.php") no-repeat;
    background-size:100% 100%;
  }
  tr{
    height: 50px;
  }
  p{
    margin: 0px;
  }
  a:link,a:visited,a:hover{
    text-decoration-line: none;
  }
  #div1{
    height: 400px;
    width: 500px;
    border: #f0f0f0 solid 10px;
    background: white;
    margin: auto;
    margin-top: 100px;
  }
  .td_left{
    text-align: right;
  }
  .input{
    border-color: #e1e1e1;
    height: 30px;
    width: 200px;
    margin-left: 30px;;
    padding-left: 10px;
    border-radius: 5px;
  }
  #div_center{
    margin-top: 50px;
  }
  #but_sub{
    width: 150px;
    height: 40px;
    background-color: #FFD026;
    border-color: #FFD026;
  }
</style>
<head>
  <meta charset="UTF-8">
  <title>登录页面</title>

  <script>
      window.onload = function (){
          var codeImg = document.getElementById("Codeimg");
          codeImg.onclick = function (){
            codeImg.src = "${pageContext.request.contextPath}/checkCodeServlet?time=" + new Date().getTime();
          }
          document.getElementById("chageCodeA").onclick = function() {
            codeImg.src = "${pageContext.request.contextPath}/checkCodeServlet?time=" + new Date().getTime();
          }
      }
  </script>
</head>
<body>
<div id="div1">
  <div id="div_center">
    <form action="${pageContext.request.contextPath}/loginServlet" method="post">
      <table align="center">
        <tr>
          <td class="td_left">用户名:</td>
          <td>
            <input type="text" placeholder="请输入账号" name="username" class="input">
          </td>
        </tr>
        <tr>
          <td class="td_left">密码:</td>
          <td>
            <input type="password" placeholder="请输入密码" name="password" class="input">
          </td>
        </tr>
        <tr>
          <td colspan="2" align="center"> <img id="Codeimg" src="${pageContext.request.contextPath}/checkCodeServlet"/>看不清 <a id="chageCodeA" href="javascript:void(0);">换一张</a> <td/>
        </tr>
        <tr>
          <td class="td_left">验证码:</td>
          <td>
            <input type="text" placeholder="请输入验证码" name="checkCode" class="input">
          </td>
        </tr>
        <tr>
          <td colspan="2" align="center">
            <span style="font-size: xx-small; color: red;">
              ${login_msg}
            </span>
          </td>
        </tr>
        <tr>
          <td colspan="2" align="center">
            <input type="submit" id="but_sub" value="登录">
          </td>
        </tr>
      </table>
    </form>
  </div>
</div>
</body>
</html>
