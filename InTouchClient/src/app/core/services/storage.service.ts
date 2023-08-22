import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  setItem(key: string, value: string) {
    localStorage.setItem(key, value)
  }

  getValue(key: string) {
    localStorage.getItem(key)
  }

  removeItem(key: string) {
    localStorage.removeItem(key)
  }

  contains(key: string) {
    return !(localStorage.getItem(key) === null)
  }

}
