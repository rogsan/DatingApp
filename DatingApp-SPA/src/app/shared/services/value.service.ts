import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ValueService {

  baseUrl = 'http://localhost:5000/api/values/';

  constructor(private http: HttpClient) { }

  getValues() {
    return this.http.get(this.baseUrl);
  }

}
