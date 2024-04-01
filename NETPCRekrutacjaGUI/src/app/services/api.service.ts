import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

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

  getContact(contactId: string) {
    return this.http.get<any>(`${this.contactUrl}${contactId}`);
  }

  updateContact(contactId : string, contact : any) {
    return this.http.put<any>(`${this.contactUrl}${contactId}`, contact);
  }

  getCategories(){
    return this.http.get<any>(this.categoryUrl);
  }

  getSubcategories(){
    return this.http.get<any>(`${this.categoryUrl}subcategory`);
  }
  
  checkEmailUniqueness(email: string, contactId?: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.contactUrl}check-email?email=${email}`+ (contactId ? `&contactId=${contactId}` : ''));
  }
  
}
