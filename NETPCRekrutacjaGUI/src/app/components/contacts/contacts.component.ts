import { AuthService } from '../../services/auth.service';
import { ApiService } from '../../services/api.service';
import { Component, OnInit } from '@angular/core';
import { UserStoreService } from 'src/app/services/user-store.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent implements OnInit {

  public contacts:any = [];

  public fullName : string = "";
  constructor(private api : ApiService, 
    public auth: AuthService, 
    private router: Router,
    private userStore: UserStoreService) { }

  ngOnInit() {
    this.api.getContacts()
    .subscribe(res=>{
      console.log(res);
      this.contacts = res;
    });

    this.userStore.getFullNameFromStore()
    .subscribe(val=>{
      const fullNameFromToken = this.auth.getfullNameFromToken();
      this.fullName = val || fullNameFromToken
    });
  }

  logout(){
    this.auth.signOut();
  }

  addContact(){
    this.router.navigate(['contact'])
  }

  goToContactDetails(id: number){
    this.router.navigate(['contact', id])
  }

}
