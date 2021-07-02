import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../components/Login.vue'
import Register from '../components/register.vue'
import Home from '../components/home.vue'
import Error1 from '../components/error.vue'
import About from '../components/about.vue'
import axios from 'axios'

Vue.use(VueRouter)

const routes = [
  { path: '/', redirect: '/home' },
  { path: '/login', component: Login },
  { path: '/register', component: Register },
  { path: '/about', component: About },
  { path: '/error', component: Error1 },
  { path: '/home', component: Home }
]

const router = new VueRouter({
  routes
})

router.beforeEach((to, from, next) => {
  if (to.path.startsWith('/register')) {
    next()
  } else if (to.path.startsWith('/login')) {
    window.localStorage.removeItem('userInfo')
    next()
  } else {
    const user = JSON.parse(window.localStorage.getItem('userInfo'))
    if (!user) {
      next({ path: '/login' })
    } else {
      axios({
        url: 'http://192.168.152.130:8332/user/checkToken',
        method: 'get',
        headers: {
          token: user.token
        }
      }).then((response) => {
        if (!response.data) {
          console.log('校验失败')
          next({ path: '/error' })
        }
      })
      next()
    }
  }
})

export default router
