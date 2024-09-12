import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Contact } from '../models/contact';
import { Observable } from 'rxjs';

const API_BASE_URL = 'https://localhost:7169/api/';

@Injectable({
  providedIn: 'root',
})
export class ContactsService {
  constructor(private httpClient: HttpClient) {}

  getContacts(): Observable<Contact[]> {
    return this.httpClient.get<Contact[]>(`${API_BASE_URL}contacts`);
  }

  uploadCsv(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    return this.httpClient.post(`${API_BASE_URL}contacts`, formData);
  }

  updateContact(contact: Contact): Observable<any> {
    return this.httpClient.put(
      `${API_BASE_URL}contacts/${contact.id}`,
      contact
    );
  }

  deleteContact(id: number): Observable<any> {
    return this.httpClient.delete(`${API_BASE_URL}contacts/${id}`);
  }
}
