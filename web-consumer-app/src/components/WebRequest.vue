<template>

  <div class="row">
    <div class="col-md-6">
      <div class="web-request-card">
      
        <ul class="nav nav-tabs">
          <li class="nav-item">
            <a v-on:click="formMode = true" v-bind:class="{'active':formMode}" class="nav-link">New Request</a>
          </li>
          <li class="nav-item">
            <a v-on:click="formMode = false" v-bind:class="{'active':!formMode}" class="nav-link">Saved Requests</a>
          </li>
        </ul>

        <div v-if="formMode">
          <form style="margin-top: 20px;">
            <div class="form-group">
              <label for="name-input">Name</label>
              <input v-model="name" type="text" class="form-control" id="name-input" placeholder="Name your request">
            </div>
            <div class="form-group">
              <label for="url-input">URL</label>
              <input v-model="url" type="text" class="form-control" id="url-input" placeholder="https://example.com">
            </div>
            <div class="form-group">
              <label for="method-input">Request Method</label>
              <select v-model="method" id="method-input" class="custom-select">
                <option value="Get">Get</option>
                <option value="Post">Post</option>
              </select>
            </div>
            <div class="form-group">
              <label for="body-input">Body</label>
              <textarea v-model="postBody" class="form-control" id="body-input" rows="5" placeholder="{some:json}" />
            </div>
            <button v-on:click="submit" class="btn btn-primary">Execute</button>
          </form>
        </div>
        
        <div v-else>
          Hello!
        </div>

      </div>

      
      
    </div>

    <div class="col-md-6">
      <results v-bind:response="response" />
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
      name: 'My Request',
      url: 'http://worldclockapi.com/api/json/est/now',
      method: 'Get',
      postBody: '',
      response: {},
      formMode: true
    }
  },
  methods: {
    submit: function() {

      event.preventDefault();

      let reqBody = {};

      if (this.postBody){
        try {
          reqBody = JSON.parse(this.postBody);
        } catch(e) {
          return;
        }
      }

      let request = {
        "Name": this.name,
        "Url": this.url,
        "Method": this.method,
        "RequestBody": reqBody
      };

      return this.$http.post("http://localhost:54846/request/execute", request)
      .then(response => {
        this.response = response.data;
        if (this.response.hasOwnProperty('Data')){
          this.response['Data'] = JSON.stringify(JSON.parse(this.response['Data']),undefined,2).trim();
        }
      });
    }
  }
}

</script>

<style>
  .web-request-card {
    margin-top: 10px;
    margin-bottom: 10px;
    padding: 20px;
    border: solid 0.5px #e2e1e0;
    border-radius: 2px;
    box-shadow: 0 10px 20px rgba(0,0,0,0.19), 0 6px 6px rgba(0,0,0,0.23);
    min-height: 400px;
  }
  .web-request-card .nav-item {
    cursor: pointer;
  }
  .active {
    font-weight: bold;
  }

</style>
