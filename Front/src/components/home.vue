<template>
  <div class="home_container">
    <h1>这是首页,欢迎回来 {{ user.username }}</h1>
    <router-link to='/about'>about</router-link>
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
.home_container{
  text-align: center;
}
</style>
