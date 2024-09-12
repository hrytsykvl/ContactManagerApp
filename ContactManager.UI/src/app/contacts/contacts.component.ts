import { Component, signal } from '@angular/core';
import { Contact } from '../models/contact';
import { ContactsService } from '../services/contacts.service';
import { CurrencyPipe, DatePipe } from '@angular/common';

@Component({
  selector: 'app-contacts',
  standalone: true,
  imports: [DatePipe, CurrencyPipe],
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.css',
})
export class ContactsComponent {
  contacts = signal<Contact[]>([]);
  editingContact: Contact | null = null;

  constructor(private contactsService: ContactsService) {}

  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts(): void {
    this.contactsService.getContacts().subscribe((data) => {
      this.contacts.set(data);
    });
  }

  editContact(contact: Contact): void {
    this.editingContact = { ...contact };
  }

  saveContact(): void {
    if (this.editingContact) {
      this.contactsService.updateContact(this.editingContact).subscribe(() => {
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
  }
}
