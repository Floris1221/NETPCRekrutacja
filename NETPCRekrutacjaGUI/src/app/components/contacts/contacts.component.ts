import { AuthService } from '../../services/auth.service';
import { ApiService } from '../../services/api.service';
import { Component, OnInit } from '@angular/core';
import { UserStoreService } from 'src/app/services/user-store.service';
import { ContactService } from 'src/app/services/contact.service';

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
    private userStore: UserStoreService,
    private contactService : ContactService) { }

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
    this.contactService.addContact();
  }

}
