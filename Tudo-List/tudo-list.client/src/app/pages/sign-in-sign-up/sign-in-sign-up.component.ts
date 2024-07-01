import { Component } from '@angular/core';
import { User } from '../../models/user/user';

@Component({
  selector: 'app-sign-in-sign-up',
  templateUrl: './sign-in-sign-up.component.html',
  styleUrl: './sign-in-sign-up.component.css'
})
export class SignInSignUpComponent {
  user: User = new User();
  isSignIn: boolean = true;
  canChangeControls: boolean = false;
  classSignIn: string = 'form-sign-in';
  classImg: string = '';

  title: string = 'ACCOUNT LOGIN';
  button: string = 'SIGN IN';
  signUpSignIn: string = "Don't have an account?";
  actionSignUpSignIn: string = "Sign up";

  onClickTextSignUpSignIn() {
    setTimeout(() => {
      this.isSignIn = !this.isSignIn;
      if (!this.isSignIn) {
        this.title = 'REGISTER';
        this.button = 'SIGN UP';
        this.signUpSignIn = 'Already has an account?';
        this.actionSignUpSignIn = "Sign in";
      } else {
        this.title = 'ACCOUNT LOGIN';
        this.button = 'SIGN IN';
        this.signUpSignIn = "Don't have an account?";
        this.actionSignUpSignIn = "Sign up";
      }
    }, 400);
    var newClassForm: string = 'form-sign-in';
    var newClassimg: string = 'form-sign-in';
    if (this.isSignIn) {
      newClassForm += ' ' + 'animate-form-right';
      newClassimg += ' ' + 'animate-img-left';
    } else {
      newClassForm += ' ' + 'animate-form-left';
      newClassimg += ' ' + 'animate-img-right';
    }
    this.classSignIn = newClassForm;
    this.classImg = newClassimg;
  }

  onClickButtonSigninSignUp() {
    console.log(this.user);
    if (this.isSignIn) {

    }
  }

  onFocusOut(idLabel: string, valueInput: string) {
    debugger
    if (valueInput) {
      document.getElementById(idLabel)?.setAttribute('class', 'label-top')
    } else {
      document.getElementById(idLabel)?.setAttribute('class', '')
    }
  }
}
