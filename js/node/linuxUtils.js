const util = require('util')
const exec = util.promisify(require('child_process').exec)

async function hasRootPriviledges() {
    const { stdout } = await exec("id -u")
    return stdout.trim() === "0"
}

module.exports = { hasRootPriviledges }
