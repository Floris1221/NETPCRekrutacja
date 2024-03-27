import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private contactUrl: string = 'https://localhost:7058/api/Contact/';
  private categoryUrl: string = 'https://localhost:7058/api/Category/';
  constructor(private http: HttpClient) {}

  getContacts() {
    return this.http.get<any>(this.contactUrl);
  }

  addContact(contact : any) {
    return this.http.post<any>(`${this.contactUrl}addContact`, contact);
  }

  getContact() {
    return this.http.get<any>(this.contactUrl);
  }

  getCategories(){
    return this.http.get<any>(this.categoryUrl);
  }
}
