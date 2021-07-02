<template>
  <div class="about_container">
    <div id="nav">
      <h1>这是下载资源的页面,欢迎回来 {{ user.username }}</h1>
      <router-link to='/home'>首页</router-link>
    </div>
    <a href="#">资源1</a> |
    <a href="#">资源2</a> |
    <a href="#">资源3</a> |
    <a href="#">资源4</a> |
    <a href="#">资源5</a> |
    <a href="#">资源6</a>
  </div>
</template>

<script>
export default {
  data () {
    return {
      user: ''
    }
  },
  created () {
    this.user = JSON.parse(localStorage.getItem('userInfo'))
  }
}
</script>

<style>
.about_container{
  text-align: center;
}
</style>
