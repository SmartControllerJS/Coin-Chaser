import controller from './controller';

const hello = () => {
    return "Hello from JS ES6 file!";
};

setText("This is a text");
//setTimeout(() => setText("And now it's changed"), 5000);


window.hello = hello;

  