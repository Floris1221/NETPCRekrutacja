import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from '../../helpers/validationform';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  public contactForm!: FormGroup;
  public categories: any[] = [];

  constructor(private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router,
    private toast: NgToastService) { }

  ngOnInit() {
    this.contactForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      category: ['', Validators.required],
      //subcategory: [''],
      //dateOfBirth: ['', Validators.required]
    });
    this.loadCategories();
  }

  loadCategories() {
    console.log("sasa");
    this.apiService.getCategories().subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (err) => {
        console.error('Error loading categories:', err);
      }
    });
  }

  onSubmit() {
    console.log("sasa " + this.contactForm.valid);
    if (this.contactForm.valid) {
      console.log(this.contactForm.value);
      this.apiService.addContact(this.contactForm.value).subscribe({
        next: (res) => {
          console.log('Kontakt dodany pomyślnie', res);
          this.contactForm.reset();
          this.toast.success({detail:"SUCCESS", summary:'Kontakt został dodany pomyślnie!', duration: 5000});
          this.router.navigate(['contacts']);
        },
        error: (err) => {
          this.toast.error({detail:"ERROR", summary:"Wystąpił błąd podczas dodawania kontaktu.", duration: 5000});
          console.log(err);
        },
      });
    } else {
      ValidateForm.validateAllFormFields(this.contactForm);
    }
  }
}
