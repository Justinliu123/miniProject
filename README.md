# miniProject
学习过程中的小小项目
## base-login
**基于java基础，一个小小的登录+表的增删改查实现。**
使用 jsp + jsp标签库 展示页面，样式库使用bootstrap, 使用JQuery框架； 业务使用druid连接池技术和JDBCTemplate获取数据库中的数据，并封装成一个javaBean对象, 简化开发、防止 sql 注入。
Filter 拦截未登录的用户转发到登录入口页面；使用 Session存储验证码信息和用户身份信息；通过 servlet、service、dao 三层结构实现了对表单信息的增删改查；
