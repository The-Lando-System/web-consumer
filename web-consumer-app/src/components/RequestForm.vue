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
      this.$requestSvc.save(this.$http, this.request)
      .then((response) => {
        this.$broadcaster.emit('savedRequest', response);
      });
    },
    submit: function() {
      event.preventDefault();
      this.$broadcaster.emit('beginLoading', {});
      if (this.request.RequestBody){
        try {
          this.request.RequestBody = JSON.parse(this.request.RequestBody);
        } catch(e) {
          return;
        }
      }

      this.$requestSvc.submit(this.$http, this.request)
      .then((response) => {
        this.$broadcaster.emit('executedRequest', response);
      });
    }
  }
}
</script>

<style>
.web-request-form {
  margin-top: 20px;
}
</style>