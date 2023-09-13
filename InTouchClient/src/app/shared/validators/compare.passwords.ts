import {FormGroup} from "@angular/forms";

export function ComparePasswords(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName]
    const matchingControl = formGroup.controls[matchingControlName]


    console.log(formGroup)

    if (matchingControl.errors && !matchingControl.errors["mustMatch"]) {
      return;
    }

    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({mustMatch: true})
    } else {
      matchingControl.setErrors({mustMatch: false})
    }
  }
}
