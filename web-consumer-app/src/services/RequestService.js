export default {
  
  requestUrl : 'http://localhost:54846/request',
  
  update(http, request) {
    return new Promise((resolve,reject) => {
      http.put(`${this.requestUrl}/${request.Id}`, request)
      .then((response) => {
        resolve(response);
      })
      .catch((error) => {
        reject(error);
      });
    }); 
  },
  save(http, request) {
    return new Promise((resolve,reject) => {
      http.post(this.requestUrl, request)
      .then((response) => {
        resolve(response);
      })
      .catch((error) => {
        reject(error);
      });
    }); 
  },
  submit(http, request) {
    return new Promise((resolve,reject) => {
      http.post(`${this.requestUrl}/execute`, request)
      .then(response => {
        response = response.data;
        if (response.hasOwnProperty('Data')){
          resolve(this.formatResponseData(response));
        } else {
          reject(response);
        }
      })
      .catch((error) => {
        reject(error);
      });
    });
  },
  getSavedRequests(http) {
    return new Promise((resolve,reject) => {
      http.get(this.requestUrl)
      .then(response => {
        resolve(response.data);
      })
      .catch((error) => {
        reject(error);
      });
    });
  },
  submitRequestById(http, id) {
    return new Promise((resolve,reject) => {
      http.post(`${this.requestUrl}/execute/${id}`, {})
      .then(response => {
        response = response.data;
        if (response.hasOwnProperty('Data')){
          resolve(this.formatResponseData(response));
        } else {
          reject(response);
        }
      });
    });
  },
  formatResponseData(response) {
    try {
      response['Data'] = JSON.stringify(JSON.parse(response['Data']),undefined,2).trim();
    } catch (e) {
      response['Data'] = response['Data'].trim();
    }
    return response;
  }
}