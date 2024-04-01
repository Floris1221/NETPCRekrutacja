import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from '../../helpers/validationform';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  public contactForm!: FormGroup;
  public categories: any[] = [];
  public subcategories: any[] = [];

  constructor(private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public auth: AuthService,
    private toast: NgToastService) { }

    public contactId: string | null = null;

  ngOnInit() {

    this.contactId = this.activatedRoute.snapshot.paramMap.get('id');

    this.contactForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), this.passwordComplexityValidator]],
      phone: ['', Validators.required],
      category: ['', Validators.required],
      subcategory: [null],
      newSubcategory: [null],
      dateOfBirth: ['', Validators.required]
    });
    this.loadCategories();
    this.loadSubcategories();

    if(this.contactId)
      this.loadContactDetails(this.contactId);

      this.watchCategoryChanges();
  }

  watchCategoryChanges() {
    this.contactForm.get('category')?.valueChanges.subscribe(selectedCategory => {
      if (selectedCategory == 1) { //should be global parameter
        this.contactForm.get('newSubcategory')?.setValue(null);
      } else if (selectedCategory == 3) { //should be global parameter
        this.contactForm.get('subcategory')?.setValue(null);
      } else {
        this.contactForm.get('subcategory')?.setValue(null);
        this.contactForm.get('newSubcategory')?.setValue(null);
      }
    });
  }

  loadCategories() {
    this.apiService.getCategories().subscribe({
      next: (data) => {
        this.categories = data;
      },
      error: (err) => {
        console.error('Error loading categories:', err);
      }
    });
  }

  loadSubcategories() {
    this.apiService.getSubcategories().subscribe({
      next: (data) => {
        this.subcategories = data;
      },
      error: (err) => {
        console.error('Error loading categories:', err);
      }
    });
  }

  loadContactDetails(contactId: string) {
    this.apiService.getContact(contactId).subscribe({
      next: (contact) => {
        this.contactForm.patchValue(contact);
      },
      error: (err) => {
        console.error('Error loading contact details:', err);
        this.toast.error({detail: "ERROR", summary: "Error loading contact details.", duration: 5000});
      }
    });
  }
  

  onSubmit() {
    if (this.contactForm.valid) {
      this.apiService.checkEmailUniqueness(this.contactForm.get("email")?.value, this.contactId || undefined).subscribe(isUniq => {
        if(!isUniq){
          this.toast.error({detail: "ERROR", summary: "Email has already been taken.", duration: 5000});
          this.contactForm.get('email')?.setErrors({ emailTaken: true });
        }else{
          const action$ = this.contactId 
        ? this.apiService.updateContact(this.contactId, this.contactForm.value)
        : this.apiService.addContact(this.contactForm.value);

        action$.subscribe({
          next: (res) => this.handleSuccess(),
          error: (err) => this.handleError("Error updated contact details.", err)
        });
      }   
    })
      
    } else {
      ValidateForm.validateAllFormFields(this.contactForm);
    }
  }


  handleSuccess() {
    console.log('Operation completed successfully');
    this.contactForm.reset();
    this.toast.success({detail: "SUCCESS", summary: 'Contact saved successfully!', duration: 5000});
    this.router.navigate(['contacts']);
  }

  handleError(summary: string, err: any) {
    console.error(summary, err);
    this.toast.error({detail: "ERROR", summary, duration: 5000});
  }


  passwordComplexityValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.value;
    if (!password) return null;
  
    const hasUpperCase = /[A-Z]/.test(password);
    const hasLowerCase = /[a-z]/.test(password);
    const hasNumeric = /[0-9]/.test(password);
    const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password);
  
    const valid = hasUpperCase && hasLowerCase && hasNumeric && hasSpecialChar;
    return valid ? null : { passwordComplexity: true };
  }

  goBack(){
    this.router.navigate(['/contacts']);
  }
}
