<template lang="html">
    <fragment>
        <div v-if="loading" class="spinner spinner-border" role="status">
            <span class="sr-only">Loading...</span>
        </div>

        <div v-else>
            <div class="outcome">
                <div v-if="error" class="error">
                    {{ error }}
                </div>

                <h1 v-if="domainData">
                    <a :href="domainData.uri" @click="handleClickLink()" target="_blank" rel="noopener noreferrer">
                        {{ domainData.host }}
                    </a>
                </h1>
            </div>

            <button class="btn btn-primary" @click="getRandomDomain()">
                {{ domainData ? 'Another one?' : 'Try Again?' }}
            </button>
        </div>

        <footer>
            Another silly project &copy; {{ currentYear }} <a href="https://www.benbristow.co.uk" target="_blank">Ben Bristow</a> <br/>
            <small>All links provided are randomly generated and I take absolutely no responsibility for their contents. Please click responsibly.</small>
        </footer>
    </fragment>
</template>

<script>
  import Vue from "vue";
  import Fragment from "vue-fragment";
  import axios from "axios";

  Vue.use(Fragment.Plugin);

  export default Vue.extend({
    data: () => ({
      domainData: null,
      error: null
    }),

    mounted() {
      this.getRandomDomain();
    },

    computed: {
      loading() {
        return !this.domainData && !this.error;
      },

      currentYear() {
        return new Date().getFullYear();
      }
    },

    methods: {
      getRandomDomain() {
        this.domainData = null;
        this.error = null;

        axios
          .get("/api/RandomDomain")
          .then(response => {
            this.domainData = response.data.data;
          })
          .catch(err => {
            if (err.response && err.response.data) {
              this.error = err.response.data.error;
            } else {
              this.error = "Unknown Error";
            }
          });
      },

      handleClickLink() {
        this.getRandomDomain();
      }
    }
  });
</script>
