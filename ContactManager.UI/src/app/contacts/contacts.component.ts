import { Component, signal } from '@angular/core';
import { Contact } from '../models/contact';
import { ContactsService } from '../services/contacts.service';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';

@Component({
  selector: 'app-contacts',
  standalone: true,
  imports: [DatePipe, CurrencyPipe, CommonModule, ReactiveFormsModule],
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.css',
})
export class ContactsComponent {
  contacts = signal<Contact[]>([]);
  editingContact: Contact | null = null;
  filteredContacts: Contact[] = [];
  selectedColumn: string = 'name';
  currentSortColumn: string | null = null;
  isSortAscending: boolean = true;
  editForm: FormGroup;

  constructor(
    private contactsService: ContactsService,
    private fb: FormBuilder
  ) {
    this.editForm = this.fb.group({
      name: new FormControl(''),
      birthDate: new FormControl(''),
      married: new FormControl(false),
      phone: new FormControl(''),
      salary: new FormControl(''),
    });
  }

  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts(): void {
    this.contactsService.getContacts().subscribe((data) => {
      this.contacts.set(data);
      this.filteredContacts = [...this.contacts()];
    });
  }

  editContact(contact: Contact): void {
    this.editingContact = { ...contact };
    this.editForm.setValue({
      name: contact.name,
      birthDate: contact.birthDate,
      married: contact.married ? true : false,
      phone: contact.phone,
      salary: contact.salary,
    });
  }

  saveContact(): void {
    if (this.editingContact) {
      const formValue = this.editForm.value;

      const updatedContact = {
        id: this.editingContact.id,
        name: formValue.name,
        birthDate: formValue.birthDate,
        married:
          formValue.married === 'true'
            ? true
            : formValue.married === 'false'
            ? false
            : formValue.married,
        phone: formValue.phone,
        salary: formValue.salary,
      };
      this.contactsService.updateContact(updatedContact).subscribe(() => {
        this.loadContacts();
        this.editingContact = null;
      });
    }
  }

  deleteContact(id: number): void {
    if (confirm('Are you sure you want to delete this contact?')) {
      this.contactsService.deleteContact(id).subscribe(() => {
        this.loadContacts();
      });
    }
  }

  cancelEdit(): void {
    this.editingContact = null;
    this.editForm.reset();
  }

  onColumnSelect(event: Event): void {
    const target = event.target as HTMLSelectElement;
    this.selectedColumn = target.value;
  }

  onFilterChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    const filterValue = target.value.toLowerCase();

    this.filteredContacts = this.contacts().filter((contact) => {
      switch (this.selectedColumn) {
        case 'name':
          return contact.name.toLowerCase().includes(filterValue);
        case 'birthDate':
          return contact.birthDate.includes(filterValue);
        case 'married':
          const isMarried = filterValue === 'yes';
          return contact.married === isMarried;
        case 'phone':
          return contact.phone.includes(filterValue);
        case 'salary':
          return contact.salary.toString().includes(filterValue);
        default:
          return false;
      }
    });
  }

  sortData(column: string): void {
    if (this.currentSortColumn === column) {
      this.isSortAscending = !this.isSortAscending;
    } else {
      this.isSortAscending = true;
      this.currentSortColumn = column;
    }
    this.filteredContacts.sort((a, b) => {
      let comparison = 0;

      switch (column) {
        case 'name':
          comparison = a.name.localeCompare(b.name);
          break;
        case 'birthDate':
          comparison = a.birthDate.localeCompare(b.birthDate);
          break;
        case 'married':
          comparison = a.married === b.married ? 0 : a.married ? 1 : -1;
          break;
        case 'phone':
          comparison = a.phone.localeCompare(b.phone);
          break;
        case 'salary':
          comparison = a.salary - b.salary;
          break;
        default:
          break;
      }

      return this.isSortAscending ? comparison : -comparison;
    });
  }
  convertToFormControl(absCtrl: AbstractControl | null): FormControl {
    const ctrl = absCtrl as FormControl;
    return ctrl;
  }
}
