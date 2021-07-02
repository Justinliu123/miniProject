<template>
<div>
</div>
</template>

<script>
export default {
  neme: 'error',
  created () {
    const _this = this
    this.$alert('登录信息失效！', '提示', {
      confirmButtonText: '确认'
    }).then((response) => {
      localStorage.removeItem('userInfo')
      _this.$router.replace({ path: '/login' })
    })
  }
}
</script>

<style>
</style>
