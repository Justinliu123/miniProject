import Vue from 'vue'
import { Button, Input, Form, FormItem, Alert, Message, MessageBox } from 'element-ui'

Vue.use(Alert)
Vue.use(Button)
Vue.use(Form)
Vue.use(FormItem)
Vue.use(Input)
Vue.prototype.$message = Message
Vue.prototype.$alert = MessageBox
