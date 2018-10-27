<template>
<form class="web-request-form">
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
</form>
</template>

<script>
export default {
  data: function() {
    return {
      requestUrl: 'http://localhost:54846/request',
      request: {
        'Name':'My Request',
        'Url':'http://worldclockapi.com/api/json/est/now',
        'Method':'Get',
        'RequestBody':''
      }
    }
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
        if (response.data.hasOwnProperty('Data')){
          response.data['Data'] = JSON.stringify(JSON.parse(response.data['Data']),undefined,2).trim();
          this.$broadcaster.emit('executedRequest', response.data);
        }
      });
    }
  }
}
</script>

<style>

</style>