import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private baseUrl: string = 'https://localhost:7058/api/User/';
  private userPayload:any;
  constructor(private http: HttpClient, private router: Router) {
   }

   addContact(){
    this.router.navigate(['contact'])
   }
}
