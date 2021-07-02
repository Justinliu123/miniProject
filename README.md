# miniProject
学习过程中的小小项目
## base-login
**基于java基础，一个小小的登录+表的增删改查实现。** <br>
访问地址入口：http://49.235.216.193:8080/ 用户名：zhangsan 密码：123
<br>
使用 jsp + jsp标签库 展示页面，样式库使用bootstrap, 使用JQuery框架； 业务使用druid连接池技术和JDBCTemplate获取数据库中的数据，并封装成一个javaBean对象, 简化开发、防止 sql 注入。<br>
Filter 拦截未登录的用户转发到登录入口页面；使用 Session存储验证码信息和用户身份信息；通过 servlet、service、dao 三层结构实现了对表单信息的增删改查；<br>

## 手机端的聊天软件
使用技术：Springboot + mysql + mybatis + netty + fastDSF存储服务器<br>
前后端分离的项目
具体介绍，请进入项目查看

## senior-login
使用vue + elementUI做前端，springboot + mybatis + mysql + redis + jwt 做后端实现的一个先后端分离的登录业务。<br>
实现了用户的注册和登录功能；用户身份认证；设置登录信息保持时间<br>
redis 和 jwt 实现了用户身份认证的功能，前端将用户的jwt存在localStorage中，后端将token作为键、用户名作为值存入redis中，并设置jwt的超时间和redis中键值对的超时时间；当用户想要访问需要用户登录的页面时，进行身份认证，未登录或者登录超时都会重新向导到登录页面。<br>
