import {IEnvoronment} from "./environment.interface";

export class EnvironmentDev implements IEnvoronment {
  apiUrl: string = "https://localhost:7211";
  hubUrl: string = "https://localhost:7211/hub";
}
