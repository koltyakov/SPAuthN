(() => {

    const exec = require('child_process').exec;

    const run = (cmd, cb) => {
        const child = exec(cmd, (error, stdout, stderr) => {
            if (stdout !== null) {
                return cb(null, stdout);
            }
            if (stderr !== null) {
                return cb(null, stderr);
            }
            if (error !== null) {
                return cb(null, error);
            }
        });
    };

    return (command, callback) => {
        run(command, callback);
    };

})();