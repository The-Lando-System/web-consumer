<template>
  <div class="container">
    
    <div class="row">
      <div class="col web-request-card">
        <div class="row">
          <h4>Url</h4>
        </div>
        <div class="row">
          <input type="text" style="width:80%" v-model="url"/>
        </div>
        <div class="row">
          <h4>Method</h4>
        </div>
        <div class="row">
          <input type="text" style="width:20%" v-model="method"/>
        </div>
        <div class="row">
          <h4>Post Body</h4>
        </div>
        <div class="row">
          <textarea style="width:50%" rows="6" v-model="postBody"/>
        </div>
        <div class="row">
          <button v-on:click="submit">
            Submit
          </button>
        </div>
      </div>
    </div>
    
    <div class="row" style="margin-top: 30px; margin-bottom: 30px;">
      <div class="col results">
        <results v-bind:responseMessage="responseMessage" />
      </div>
    </div>

  </div>
</template>

<script>
import Results from './Results.vue';

export default {
  components: {
    Results
  },
  data: function() {
    return {
      url: 'http://worldclockapi.com/api/json/est/now',
      method: 'Get',
      postBody: '',
      responseMessage: ''
    }
  },
  methods: {
    submit: function() {

      let reqBody = {};

      try {
        reqBody = JSON.parse(this.postBody);
      } catch(e) {}

      let request = {
        "Url": this.url,
        "Method": this.method,
        "RequestBody": reqBody
      };

      return this.$http.post("http://localhost:54846/request/submit", request)
      .then(response => {
        if (response.data.hasOwnProperty('Data')) {
          this.responseMessage = JSON.stringify(JSON.parse(response.data['Data']),undefined,2).trim();
        }
      });
    }
  }
}

</script>

<style>
  .web-request-card {
    margin-top: 30px;
    padding-top: 20px;
    padding-bottom: 20px;
    border: solid 0.5px #e2e1e0;
    border-radius: 2px;
    box-shadow: 0 10px 20px rgba(0,0,0,0.19), 0 6px 6px rgba(0,0,0,0.23);
  }
  .web-request-card .row {
    margin-left: 20px;
  }
  .web-request-card input {
    margin-bottom: 20px;
  }
  .web-request-card button {
    margin-top: 20px;
  }
  .results {
    border: solid 0.5px #e2e1e0;
    border-radius: 2px;
    text-align: left;
    padding: 20px;
  }
</style>
