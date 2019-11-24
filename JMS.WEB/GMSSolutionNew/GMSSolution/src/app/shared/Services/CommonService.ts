import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class CommonService {

    private componentName = new BehaviorSubject('Login');
    currentComponent = this.componentName.asObservable();

    constructor() { }

    setComponentName(component: string) {
        this.componentName.next(component)
    }

}
