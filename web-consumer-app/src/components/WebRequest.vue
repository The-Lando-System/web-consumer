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
          <request-form/>
          <!-- <form class="web-request-form">
            <div class="form-group">
              <label for="name-input">Name</label>
              <input v-model="request.Name" type="text" class="form-control" id="name-input" placeholder="Name your request">
            </div>
            <div class="form-group">
              <label for="url-input">URL</label>
              <input v-model="request.Url" type="text" class="form-control" id="url-input" placeholder="https://example.com">
            </div>
            <div class="form-group">
              <label for="method-input">Request Method</label>
              <select v-model="request.Method" id="method-input" class="custom-select">
                <option value="Get">Get</option>
                <option value="Post">Post</option>
              </select>
            </div>
            <div class="form-group">
              <label for="body-input">Body</label>
              <textarea v-model="request.RequestBody" class="form-control" id="body-input" rows="5" placeholder="{some:json}" />
            </div>
            <button v-on:click="submit" class="btn btn-primary">Execute</button>
            <button v-on:click="save" style="float:right" class="btn btn-info">Save</button>
          </form> -->
        </div>
        
        <div v-else>
          <div class="saved-requests-area">
            <div class="list-group">
              <a href="javascript:void(0);" v-for="(request, index) in savedRequests" v-bind:key="request.Name" v-bind:class="activeButtonIndex === index ? 'active' : ''" v-on:click="activeButtonIndex = index; activeRequest = request;" class="list-group-item list-group-item-action">{{request.Name}}</a>
            </div>
            <button v-on:click="submit()" class="btn btn-primary">Execute</button>
          </div>
        </div>

      </div>
      
    </div>

    <div class="col-md-6">
      <results />
    </div>
    
  </div>
</template>

<script>
import Results from './Results.vue';
import RequestForm from './RequestForm.vue';
//import Events from '../Events.js';

export default {
  components: {
    Results,
    RequestForm
  },
  data: function() {
    return {
      request: {
        'Name':'My Request',
        'Url':'http://worldclockapi.com/api/json/est/now',
        'Method':'Get',
        'RequestBody':''
      },
      name: 'My Request',
      url: 'http://worldclockapi.com/api/json/est/now',
      method: 'Get',
      postBody: '',
      response: {},
      formMode: true,
      activeButtonIndex: 0,
      activeRequest: {},
      savedRequests: [],
      requestUrl: 'http://localhost:54846/request'
    }
  },
  computed: {
    activeTab: function(index) {
      return {
        'active':  index == this.activeButtonIndex
      }
    }
  },
  created: function () {
    this.getSavedRequests();
    this.$broadcaster.on('executedRequest', (data) => {
      this.response = data;
    });
  },
  methods: {
    save: function() {
      event.preventDefault();
      console.log("saving request");
      return this.$http.post(this.requestUrl, this.request)
      then(response => {
        console.log(response);
      });
    },
    getSavedRequests: function() {
      console.log("getting requests");
      return this.$http.get(this.requestUrl)
      .then(response => {
        this.savedRequests = response.data;
      });
    },
    submitWithId: function(id) {
      event.preventDefault();
      console.log("executing request");
      return this.$http.post(`${this.requestUrl}/execute/${id}`, {})
      .then(response => {
        this.response = response.data;
        if (this.response.hasOwnProperty('Data')){
          this.response['Data'] = JSON.stringify(JSON.parse(this.response['Data']),undefined,2).trim();
        }
      });
    },
    submit: function() {
      event.preventDefault();
      if (this.request.RequestBody){
        try {
          this.request.RequestBody = JSON.parse(this.request.RequestBody);
        } catch(e) {
          return;
        }
      }

      return this.$http.post(`${this.requestUrl}/execute`, this.request)
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
  .web-request-card .nav-item,
  button {
    cursor: pointer;
  }
  .active {
    font-weight: bold;
  }
  .web-request-form {
    margin-top: 20px;
  }
  .saved-requests-area {
    margin-top: 20px;
  }
  .saved-requests-area button {
    margin-top: 20px;
  }
  .list-group {
    max-height: 400px;
    overflow: auto;
  }
</style>
