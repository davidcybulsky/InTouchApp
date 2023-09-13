import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() {
  }

  setItem(key: string, value: string): void {
    localStorage.setItem(key, value)
  }

  getValue(key: string): string | null {
    return localStorage.getItem(key)
  }

  removeItem(key: string): void {
    localStorage.removeItem(key)
  }

  contains(key: string): boolean {
    return !(localStorage.getItem(key) === null)
  }

}
