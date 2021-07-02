<template>
<div class = "register_container">
    <div class="login_box">
      <el-form :model="ruleForm" status-icon :rules="rules" ref="ruleForm" label-width="80px" class="login_form">
        <h2 align="center">注册</h2>
        <el-form-item label="用户名" prop="username">
          <el-input v-model.number="ruleForm.username"></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input type="password" v-model="ruleForm.password" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="确认密码" prop="checkPass">
          <el-input type="password" v-model="ruleForm.checkPass" autocomplete="off"></el-input>
        </el-form-item>
        <!-- 按钮 -->
        <el-form-item class="btns">
            <el-button type="primary" @click="register">注册</el-button>
            <el-button @click="goto_login">返回</el-button>
        </el-form-item>
      </el-form>
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
    var validatePass2 = (rule, value, callback) => {
      if (value === '') {
        callback(new Error('请再次输入密码'))
      } else if (value !== this.ruleForm.password) {
        callback(new Error('两次输入密码不一致!'))
      } else {
        callback()
      }
    }
    return {
      isWarn: false,
      warnInfo: '',
      ruleForm: {
        username: '',
        password: '',
        checkPass: ''
      },
      rules: {
        password: [
          { validator: validatePass, trigger: 'blur' }
        ],
        checkPass: [
          { validator: validatePass2, trigger: 'blur' }
        ],
        username: [
          { validator: checkAge, trigger: 'blur' }
        ]
      }
    }
  },
  methods: {
    goto_login () {
      this.$router.push('/login')
    },
    register () {
      this.$refs.ruleForm.validate(async valid => {
        // 提交前进行表单验证
        if (!valid) return

        const { data: res } = await this.$http.post('/user/register', this.ruleForm)
        // console.log(res)
        if (!res.success) {
          this.$message.error(res.message)
        } else {
          this.$message.success(res.message)
          this.$router.push('/login')
        }
      })
    }
  }
}
</script>

<style>
.register_container{
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
    bottom: 0px;
    width: 100%;
    padding: 0 30px 0 20px;
    box-sizing: border-box;
}
.btns {
    display: flex;
    justify-content: flex-end;
}
</style>
