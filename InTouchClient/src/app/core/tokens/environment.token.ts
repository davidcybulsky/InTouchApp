import {InjectionToken} from "@angular/core"
import {IEnvoronment} from "src/environment/environment.interface";

export const ENVIRONMENT_TOKEN = new InjectionToken<IEnvoronment>('environment.token');
