<template>
  <div class = "login_container">
      <div class="login_box">
          <el-form :model="ruleForm" status-icon :rules="rules" ref="ruleForm" label-width="80px" class="login_form">
            <h2 align="center">请登录</h2>
            <el-form-item label="用户名" prop="username">
              <el-input v-model.number="ruleForm.username"></el-input>
            </el-form-item>
            <el-form-item label="密码" prop="password">
              <el-input type="password" v-model="ruleForm.password" autocomplete="off"></el-input>
            </el-form-item>
            <!-- 按钮 -->
            <el-form-item class="btns">
                <el-button type="primary" @click="login">登录</el-button>
                <el-button @click="goto_register">注册</el-button>
            </el-form-item>
          </el-form>
          <el-alert v-show="isWarn" :title="warnInfo" type="warning" class="warnInfo" :closable="false" center show-icon></el-alert>
      </div>
  </div>
</template>

<script>
export default {
  data () {
    var checkAge = (rule, value, callback) => {
      if (value === '') {
        return callback(new Error('用户名不能为空'))
      } else {
        if (value.length > 64) {
          callback(new Error('用户名过长'))
        }
        callback()
      }
    }
    var validatePass = (rule, value, callback) => {
      if (value === '') {
        callback(new Error('请输入密码'))
      } else {
        if (value.length < 6) {
          callback(new Error('密码必须大于6位'))
        } else if (value.length >= 64) {
          callback(new Error('密码必须小于64位'))
        }
        callback()
      }
    }
    return {
      isWarn: false,
      warnInfo: '',
      ruleForm: {
        username: '',
        password: ''
      },
      rules: {
        password: [
          { validator: validatePass, trigger: 'blur' }
        ],
        username: [
          { validator: checkAge, trigger: 'blur' }
        ]
      }
    }
  },
  methods: {
    login () {
      this.$refs.ruleForm.validate(async valid => {
        // 提交前进行表单验证
        if (!valid) return

        const { data: res } = await this.$http.post('/user/login', this.ruleForm)
        // 登录失败与否，通过返回值的success来判断
        // console.log(res)
        if (!res.success) {
          this.warnInfo = res.message
          this.isWarn = true
        } else {
          this.$message.success(res.message)
          localStorage.setItem('userInfo', JSON.stringify(res.result))
          this.$router.push('/home')
        }
      })
    },
    goto_register () {
      this.$router.push('/register')
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="less" scoped>
.login_container{
    height: 100%;
    background-color: #2b4b6b;
}
.login_box{
    width: 500px;
    height: 350px;
    background-color: #fff;
    border-radius: 3px;
    position: absolute;
    left:50%;
    top: 50%;
    transform: translate(-50%,-50%);
}
.login_form{
    position: absolute;
    bottom: 50px;
    width: 100%;
    padding: 0 50px 0 10px;
    box-sizing: border-box;
}
.btns {
    display: flex;
    justify-content: flex-end;
}
.warnInfo {
  height: 50px;
  position: absolute;
  bottom: 0;
}
</style>
