import './scss/main.scss';

import Vue from 'vue';
import App from './js/App.vue';

new Vue({ render: createElement => createElement(App) }).$mount('#app');